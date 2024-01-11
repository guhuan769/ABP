using Elon.Production.Samples;
using Xunit;

namespace Elon.Production.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<ProductionMongoDbTestModule>
{

}
