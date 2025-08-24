using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using System.Net;
using AdvancedTodoApp.Application.Common.Models;
using AdvancedTodoApp.Application.Common;

namespace AdvancedTodoApp.Application.Common.Behaviors
{
    /// <summary>
    /// This class runs before every MediatR request(command or query) and validates it using FluentValidation
    /// </summary>
    /// <typeparam name="TRequest"> MediatR request(e.g. CreateCategoryCommand </typeparam>
    /// <typeparam name="TResponse"> The response, e.g OperationResult<CategoryDto> </CategoryDto> implements IOperationResult</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Runs the validators before moving on to the next handler
        /// </summary>
        /// <param name="request"> The MediatR request being handled(command or query)</param>
        /// <param name="next"> The next delegate in pipeline(usually the Handler for the command/query)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("validator testline testtest test");
            if (_validators.Any())
            {
                // Run all the validators in parallel on the incomming request
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                );
                // Collect all the validation errors so we can return them all at once
                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                // If there are any errors, we return an error response
                if (failures.Count != 0)
                {
                    var responseType = typeof(TResponse);

                    if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(OperationResult<>))
                    {
                        var innerType = responseType.GenericTypeArguments[0];

                        // Create instance of OperationResult<T>
                        var resultInstance = Activator.CreateInstance(responseType);

                        // Add validation errors to the error list
                        var addErrorMethod = responseType.GetMethod("AddError");

                        foreach (var error in failures)
                        {
                            addErrorMethod?.Invoke(resultInstance, new object[] { error.ErrorMessage });
                        }

                        return (TResponse)resultInstance;
                    }

                    // if TResponse is not OperationResult<T>, throw exception
                    throw new InvalidOperationException($"ValidationBehavior expected TResponse to be OperationResult<T>, but got {typeof(TResponse).Name}");
                }
            }
            return await next();
        }
    }
}
