using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaptiveCards.Templating;

namespace AdaptiveCardsTemplatingBugDemo
{
    [TestClass]
    public class AdaptiveCardsTemplatingUnitTest
    {
        [TestMethod]
        public void TestExtraCommaInTheMiddle()
        {
            string jsonTemplate = @"{
                ""type"": ""AdaptiveCard"",
                ""body"": [
                    {
                        ""type"": ""ColumnSet"",
                        ""$data"": ""${foo}"",
                        ""$when"": ""${$index==0}""
                    },
                    {
                        ""type"": ""Container"",
                        ""$data"": ""${foo}"",
                        ""$when"": ""${$index>0}""
                    }
                ]
            }";

            string jsonData = @"{
                ""foo"": [{}, {}]
            }";

            AdaptiveCardTemplate transformer = new AdaptiveCardTemplate(jsonTemplate);
            var context = new EvaluationContext
            {
                Root = jsonData
            };

            string cardJson = transformer.Expand(context);

            Assert.IsFalse(cardJson.Contains(",,"));
        }

        [TestMethod]
        public void TestExtraCommaInTheEnd()
        {
            string jsonTemplate = @"{
                ""type"": ""AdaptiveCard"",
                ""body"": [{
                    ""type"": ""Container"",
                    ""items"": [{
                        ""type"": ""Container"",
                        ""$when"": ""${bar==1}""
                    }, {
                        ""type"": ""ColumnSet"",
                        ""$when"": ""${bar==2}""
                    }],
                    ""$data"": ""${foo}""
                }]
            }";

            string jsonData = @"{
                ""foo"": [{
                    ""bar"": 1
                }]
            }";

            AdaptiveCardTemplate transformer = new AdaptiveCardTemplate(jsonTemplate);
            var context = new EvaluationContext
            {
                Root = jsonData
            };

            string cardJson = transformer.Expand(context);

            Assert.IsFalse(cardJson.Contains(",]"));
        }

        [TestMethod]
        public void TestMissingElement()
        {
            string jsonTemplate = @"{
                ""type"": ""AdaptiveCard"",
                ""body"": [{
                    ""type"": ""Container"",
                    ""$when"": ""${$index>0}"",
                    ""items"": [{
                        ""type"": ""TextBlock"",
                        ""text"": ""Index ${$index}""
                    }],
                    ""$data"": ""${foo}""
                }]
            }";

            string jsonData = @"{
                ""foo"": [1, 1]
            }";

            AdaptiveCardTemplate transformer = new AdaptiveCardTemplate(jsonTemplate);
            var context = new EvaluationContext
            {
                Root = jsonData
            };

            string cardJson = transformer.Expand(context);

            Assert.IsTrue(cardJson.Contains("Index 1"));
        }
    }
}
