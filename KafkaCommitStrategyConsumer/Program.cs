ConsumeWithAutoCommitAndManualOffsetStore();

static void ConsumeWithAutoCommitAndManualOffsetStore()
{
    var cancellationToken = new CancellationTokenSource().Token;

    var config = new ConsumerConfig
    {
        BootstrapServers = "host1:9092,host2:9092",
        GroupId = "kafkacommitstrategy",
        EnableAutoCommit = true,
        EnableAutoOffsetStore = false,
        AutoCommitIntervalMs = 1000,
        AutoOffsetReset = AutoOffsetReset.Earliest
    };

    using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

    consumer.Subscribe("autocommitandmanualoffsetstore");

    while (true)
    {
        var consumeResult = consumer.Consume(cancellationToken);

        if (consumeResult is not null)
        {
            consumer.StoreOffset(consumeResult);
        }
    }
}