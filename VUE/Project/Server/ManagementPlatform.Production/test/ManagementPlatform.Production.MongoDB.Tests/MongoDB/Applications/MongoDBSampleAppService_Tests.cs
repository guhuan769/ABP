using ManagementPlatform.Production.MongoDB;
using ManagementPlatform.Production.Samples;
using Xunit;

namespace ManagementPlatform.Production.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<ProductionMongoDbTestModule>
{

}
