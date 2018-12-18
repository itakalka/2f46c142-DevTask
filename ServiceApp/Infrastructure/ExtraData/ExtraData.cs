namespace ServiceApp.Infrastructure.ExtraData
{
    public sealed class ExtraData
    {
        public string IdempotencyKey { get; set; }

        public string Description { get; set; }
    }
}