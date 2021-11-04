# AdaptiveCardsTemplatingBugDemo
> POC for bugs in `AdaptiveCards.Templating.Expand`

## Environments
- .Net5.0
- Newtonsoft.Json 13.0.1
- AdaptiveCards.Templating 1.2.2

## Description
This project contains 3 test cases. All of them are failed due to the bugs in `AdaptiveCards.Templating.Expand`.

`TestExtraCommaInTheMiddle` and `TestExtraCommaInTheEnd` is about getting some extra commas in the returned json, as stated in [Issue #6680](https://github.com/microsoft/AdaptiveCards/issues/6680).

`TestMissingElement` is about missing some elements in the returned card, as stated in [Issue #6684](https://github.com/microsoft/AdaptiveCards/issues/6684).
