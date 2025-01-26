using BankAccountTransactions.Domain.Dto;
using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Repository;
using BankAccountTransactions.Domain.Service;

namespace BankAccountTransactions.Application.UseCase
{
    public class CreateTransactionUseCase
    {
        private readonly GetUserByDocumentUseCase _getUserByDocumentUseCase;
        private readonly GetUserAccountUseCase _getUserAccountUseCase;
        private readonly UpdateUserAccountBalanceUseCase _updateUserAccountBalanceUseCase;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly INotificationService _notificationService;

        public CreateTransactionUseCase(
            GetUserByDocumentUseCase getUserByDocumentUseCase,
            GetUserAccountUseCase getUserAccountUseCase,
            UpdateUserAccountBalanceUseCase updateUserAccountBalanceUseCase,
            ITransactionRepository transactionRepository,
            IAuthorizationService authorizationService,
            INotificationService notificationService)
        {
            _getUserByDocumentUseCase = getUserByDocumentUseCase;
            _getUserAccountUseCase = getUserAccountUseCase;
            _updateUserAccountBalanceUseCase = updateUserAccountBalanceUseCase;
            _transactionRepository = transactionRepository;
            _authorizationService = authorizationService;
            _notificationService = notificationService;
        }

        public async Task<Transaction> Execute(TransactionDto transactionDto)
        {
            // Validações iniciais
            if (transactionDto.Amount <= 0)
            {
                throw new ArgumentException("O valor da transação deve ser maior que zero.", nameof(transactionDto.Amount));
            }

            // Obtém os usuários
            var sender = await _getUserByDocumentUseCase.Execute(transactionDto.SenderDocument);
            var receiver = await _getUserByDocumentUseCase.Execute(transactionDto.ReceiverDocument);

            if (sender == null || receiver == null)
            {
                throw new ArgumentException("Usuários não encontrados para a transação.");
            }

            var senderAccount = await _getUserAccountUseCase.Execute(transactionDto.SenderDocument);
            var receiverAccount = await _getUserAccountUseCase.Execute(transactionDto.ReceiverDocument);

            if (sender == null || receiver == null)
            {
                throw new ArgumentException("Contas não encontradas para a transação.");
            }

            // Verifica saldo suficiente
            if (senderAccount.Balance < transactionDto.Amount)
            {
                throw new InvalidOperationException("Saldo insuficiente para a transação.");
            }

            // Autoriza a transação
            var isAuthorized = await _authorizationService.AuthorizeTransactionAsync();
            if (!isAuthorized)
            {
                throw new Exception("Transaction unauthorized.");
            }
            var senderIsNotificated = await _notificationService.SendNotificationAsync(sender!, "Transação realizada com sucesso.");
            var receiverIsNotificated = await _notificationService.SendNotificationAsync(receiver!, "Você recebeu uma nova transação.");
            if (!senderIsNotificated || !receiverIsNotificated)
            {
                throw new Exception("Error sending notifications.");
            }
            // Atualiza saldos
            sender.Account.Balance -= transactionDto.Amount;
            receiver.Account.Balance += transactionDto.Amount;

            await _updateUserAccountBalanceUseCase.Execute(sender.Document, sender.Account.Balance);
            await _updateUserAccountBalanceUseCase.Execute(receiver.Document, receiver.Account.Balance);

            // Cria a transação
            var transaction = new Transaction(Guid.NewGuid(), transactionDto.SenderDocument, transactionDto.ReceiverDocument, transactionDto.Amount, DateTime.Now);


            await _transactionRepository.Insert(transaction);
            return transaction;

        }
    }
}