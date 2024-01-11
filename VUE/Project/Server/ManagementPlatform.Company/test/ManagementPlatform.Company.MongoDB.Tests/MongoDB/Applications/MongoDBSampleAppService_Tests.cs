using ManagementPlatform.Company.MongoDB;
using ManagementPlatform.Company.Samples;
using Xunit;

namespace ManagementPlatform.Company.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<CompanyMongoDbTestModule>
{

}
