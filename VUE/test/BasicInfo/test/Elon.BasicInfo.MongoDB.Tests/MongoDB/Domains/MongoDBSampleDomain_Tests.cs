using Elon.BasicInfo.Samples;
using Xunit;

namespace Elon.BasicInfo.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<BasicInfoMongoDbTestModule>
{

}
