
using FusioWorker.Generated;

namespace FusioWorker
{
    interface IAction
    {
        public Response Handle(Request request, Context context);
    }
}
