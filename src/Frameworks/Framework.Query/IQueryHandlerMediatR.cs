using Framework.Queries;
using MediatR;

namespace Framework.Query
{
    public interface IQueryHandlerMediatR<in TQueryRequest, TQueryResponse> :IRequestHandler<TQueryRequest, TQueryResponse> 
        where TQueryRequest:IRequest<TQueryResponse> where TQueryResponse:BaseResponseQuery
    {
        
    }
}