namespace IndeedId.Test
{
	public static class Constants
	{
		public const string WalletNotExistsMsg = "Кошелек с данной валютой не найден среди кошельков пользователя.";
		public const string WalletHasNotEnoughMoneyMsg = "В кошельке с данной валютой недостаточно средств для снятия.";
		public const string ExchangeApiUrl = "https://api.exchangeratesapi.io/latest";
		public const string CurrencyNotSupported = "Одна из выбранных валют не поддерживается.";
		public const string AmountShouldBePositive = "Сумма для внесения должна быть больше нуля.";
	}
}