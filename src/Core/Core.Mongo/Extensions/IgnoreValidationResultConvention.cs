using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace Core.Mongo
{
    public class IgnoreValidationResultConvention : ConventionBase, IMemberMapConvention
    {
        public void Apply(BsonMemberMap memberMap)
        {
            if (memberMap.MemberName == "ValidationResult")
                memberMap.SetShouldSerializeMethod(o => false);
        }
    }
}