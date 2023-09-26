# Randomizer Logic Parser

### How to use
1. Create a class derived from "LogicParser.InventoryData" that will handle evaluating expressions
2. Override the "GetVariable" method to return a bool or int based on the current value of the specified variable
3. Call the "Evaluate" method with a logical expression to evaluate it

### Supported operators
- Parenthesis: ```()``` or ```[]```
- And: ```+``` or ```&&```
- Or: ```|``` or ```||```
- Less than: ```<```
- Greater than: ```>```
- Less than or equal: ```<=```
- Greater than or equal: ```>=```