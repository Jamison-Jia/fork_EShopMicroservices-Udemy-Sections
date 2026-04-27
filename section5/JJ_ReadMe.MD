## Common - BuildingBlocks
### Nuget packages:
* Carter - endpoint
    ```
    ...

    builder.Services.AddCarter();

    ...

    app.MapCarter();

    ```
* MediatR - middleware for http request pipelines
    ```
    var assembly = typeof(Program).Assembly;
    builder.Services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });
    ```
* Marten - DocumentationDB PostgreSQL
### Fluent Validations
* Add nuget package - FluentValidation
* Add ValidationBehavior, we can use where to limit what type are need to be validated
* Registry ValidationBehavior in certain project refering BuildingBlocks
```CSharp
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    ...
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    ...
});
builder.Services.AddValidatorsFromAssembly(assembly);
```
* Add specific validators in cersion project
```C#
public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

```
### Global Exception handlers
* Implement diff kinds of exceptions
* Resgistry and use exception handlers in centain proj
```
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
...
app.UseExceptionHandler(option => { });
...
```


## CatalogApi
### DockerCompose for CatalogApi
* Add docker support file in CatalogApi.proj
* Add docker compose support file in CatalogApi.proj
* Edit docker compose override file by:
    * Set depends_on
    * Change ports when necesary
    * Add ConnectionStrings to override the same config in appsetttings.jon in Api.Proj
* BUild and Run api proj as docker compose
* Test Apis via Postman

## Docker Compose
* In VS, right click on the certain proj -> add -> Docker support
* Add Docker compose support
* Add addtional cofigs in override docker compose like
    * ConnectionStrings
    * Depends_on
* Run docker compose and check status/logs of containers.