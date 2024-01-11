using ManagementPlatform.Production.Samples;
using Xunit;

namespace ManagementPlatform.Production.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<ProductionMongoDbTestModule>
{

}
