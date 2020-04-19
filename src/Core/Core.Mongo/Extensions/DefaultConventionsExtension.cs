using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Core.Mongo.Extensions
{
    public static class DefaultConventionsExtension
    {
        public static void RegisterDefaultConventions(this IMongoClient client)
        {
            ConventionRegistry.Register("IgnoreValidationResult", new ConventionPack { new IgnoreValidationResultConvention() }, t => true);
            ConventionRegistry.Register("IgnoreIfNull", new ConventionPack { new IgnoreIfNullConvention(true) }, t => true);
            ConventionRegistry.Register("EnumStringConvention", new ConventionPack { new EnumRepresentationConvention(BsonType.String) }, t => true);
            ConventionRegistry.Register("IgnoreExtraElements", new ConventionPack { new IgnoreExtraElementsConvention(true) }, t => true);
        }
    }
}
