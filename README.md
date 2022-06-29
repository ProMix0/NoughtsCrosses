# NoughtsCrosses
Simple game realisation for many platforms

...or just noughts-and-crosses based example of use multiple frameworks

# Usage
You can inject all game services into you DI container
> Manual dependency resolving is not recommended
```csharp
.ConfigureServices((context, services) =>
        services
            .AddNoughtsCrossesGame(builder =>
                    builder
                        .AddPlayer<AiPlayer>()
                        .AddPlayer<WpfPlayer>()
                        .AddField<OptimizedField>()
                        .AddGame<Game<AiPlayer, WpfPlayer>>(),
                addLoopingService: true)
    )
```
Then just run your `Host`
