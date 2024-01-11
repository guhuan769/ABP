using Elon.Production.MongoDB;
using Elon.Production.Samples;
using Xunit;

namespace Elon.Production.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<ProductionMongoDbTestModule>
{

}
