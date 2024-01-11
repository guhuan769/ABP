using ManagementPlatform.Company.Samples;
using Xunit;

namespace ManagementPlatform.Company.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<CompanyMongoDbTestModule>
{

}
