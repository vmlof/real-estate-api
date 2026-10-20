using Homely.Domain.Common;

namespace Homely.Api.Extensions;

public static class ResultExtensions
{
    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Error, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
    }
}

// return result.Match<LoginResponse, ActionResult>(
//     onSuccess: response => Ok(response),
//     onFailure: error => Unauthorized(new
//     {
//         error.Code,
//         error.Description
//     })
// );