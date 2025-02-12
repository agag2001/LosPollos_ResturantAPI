

namespace LosPollos.Domain.Exceptions
{
    public  class NotFoundException:Exception
    {
        public NotFoundException(string resourceType,string resourceId):
            base($" the Resource {resourceType} with Id {resourceId} is not found")
        {
            
        }
    }
}
