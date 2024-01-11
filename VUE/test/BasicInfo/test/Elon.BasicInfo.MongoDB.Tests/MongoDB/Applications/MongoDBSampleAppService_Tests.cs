using Elon.BasicInfo.MongoDB;
using Elon.BasicInfo.Samples;
using Xunit;

namespace Elon.BasicInfo.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<BasicInfoMongoDbTestModule>
{

}
