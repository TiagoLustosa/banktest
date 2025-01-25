using BankAccountTransactions.Domain.Dto;
using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Repository;
using BankAccountTransactions.Domain.Service;

namespace BankAccountTransactions.Application.UseCase
{
    public class CreateTransactionUseCase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly INotificationService _notificationService;

        public CreateTransactionUseCase(
            IAccountRepository accountRepository,
            IUserRepository userRepository,
            ITransactionRepository transactionRepository,
            IAuthorizationService authorizationService,
            INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _authorizationService = authorizationService;
            _notificationService = notificationService;
        }

        public async Task<Transaction> ExecuteAsync(TransactionDto transactionDto)
        {
            // Validações iniciais
            if (transactionDto.Amount <= 0)
            {
                throw new ArgumentException("O valor da transação deve ser maior que zero.", nameof(transactionDto.Amount));
            }

            // Obtém os usuários
            var senderAccount = await _accountRepository.GetByDocument(transactionDto.SenderDocument);
            var receiverAccount = await _accountRepository.GetByDocument(transactionDto.ReceiverDocument);

            if (senderAccount == null || receiverAccount == null)
            {
                throw new ArgumentException("Usuários não encontrados para a transação.");
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
                throw new Exception("Transação não autorizada.");
            }

            // Atualiza saldos
            senderAccount.Balance -= transactionDto.Amount;
            receiverAccount.Balance += transactionDto.Amount;

            await _accountRepository.Update(senderAccount);
            await _accountRepository.Update(receiverAccount);

            // Cria a transação
            var transaction = new Transaction(Guid.NewGuid(), transactionDto.SenderDocument, transactionDto.ReceiverDocument, transactionDto.Amount, DateTime.Now);


            await _transactionRepository.Insert(transaction);
            var senderUser = await _userRepository.GetByDocument(transactionDto.SenderDocument);
            var receiverUser = await _userRepository.GetByDocument(transactionDto.ReceiverDocument);
            // Envia notificações
            await _notificationService.SendNotificationAsync(senderUser!, "Transação realizada com sucesso.");
            await _notificationService.SendNotificationAsync(receiverUser!, "Você recebeu uma nova transação.");

            return transaction;
        }
    }
}