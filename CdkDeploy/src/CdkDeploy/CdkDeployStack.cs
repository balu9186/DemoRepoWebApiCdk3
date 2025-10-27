using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Constructs;

namespace CdkDeploy
{
    public class CdkDeployStack : Stack
    {
        internal CdkDeployStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here

            var lambdaFunction = new Function(this, "AspNetCoreLambda3", new FunctionProps
            {
                Runtime = Runtime.DOTNET_8,
                Handler = "WebApi::WebApi.LambdaEntryPoint::FunctionHandlerAsync",
                Code = Code.FromAsset("../WebApi/src/WebApi/bin/Release/net8.0"),
                MemorySize = 512,
                Timeout = Duration.Seconds(30)
            });

            new LambdaRestApi(this, "AspNetCoreApiGateway3", new LambdaRestApiProps
            {
                Handler = lambdaFunction,
                Proxy = true

            });
        }
    }
}
