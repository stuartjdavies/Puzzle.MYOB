# Children's game puzzle

## Consider the following children’s game: 

n children stand around a circle. 
Starting with a given child and working clockwise, each child gets a sequential number, which we will refer to as it’s id. 
Then starting with the first child, they count out from 1 until k. The  k’th child is now out and leaves the circle. The count starts again with the child immediately next to the eliminated one.
Children are so removed from the circle one by one. The winner is the  last child left standing.
Write some code which, when given n and k, returns:
the sequence of children as they are eliminated, and 
The id of the winning child.

Please provide a program to run your solution that outputs the winner and sequence of eliminated children. Create any classes you need to support the design of your solution and also any unit tests.  We are looking for a well-designed, testable and maintainable code.

## For bonus points: 
Use as little memory as possible and make it run as fast as possible.
In your comments, discuss the runtime order complexity of your solution e.g., O(n) or O(n^2), etc.
Explain any assumptions or trade-offs you have made.
