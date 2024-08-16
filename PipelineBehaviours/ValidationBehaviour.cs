using FluentValidation;
using MediatR;

namespace BrightStarPhase1App.PipelineBehaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationRes = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));
                var failures = validationRes.SelectMany(x => x.Errors).Where(y => y != null).ToList();

                if (failures.Count != 0)
                {
                    throw new Exception(failures.First().ErrorMessage);
                }
            }
            return await next();
        }
    }
}
