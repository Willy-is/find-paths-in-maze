# Find paths in the maze

- Tech stack: C#
- Problem-solving task

## Scenario

- Start in [0,0] to get out to the exit [R-1,C-1]
- Can move from his/her current cell to one of the horizontally or vertically adjacent cells
- Cannot walk into a wall, but can break only one wall on his/her way

## Assumption

Instead of one person from the start to the end, it came up with two people at [0,0] and [R-1,C-1] walking towards the walls. </br>
`Path == No walls && not visited yet`

## Solution

1. Adjacent cell == Path </br>
   If the neighbouring cell is a _path_, set the current cell as **_visited_** and move onto that cell. Then, the person can check its adjacent cells again. It is noticed that he/she cannot go back to the previous cell (marked as **_visited_**) until there is any _path_ available. Therefore, he/she only can go back once he/she is stuck

2. Adjacent cell = Wall </br>
   If the adjacent cell is a _wall_, the person goes back to the previous cell and marks the wall as **_visited_** as he/she can break only one wall on his/her way

3. Count the number of breaking a wall to the exit
   As the figure below, when there is the same wall that both people can get access to, there is a way to get out because it means there is a route from the entrance to the wall and also from the wall to the exit

<img width="500" alt="one-wall" src="https://user-images.githubusercontent.com/57608628/149684824-3bd849d3-cab0-478b-9dea-1775488043e7.png">

_Figure 1. The case of one wall_

<img width="500" alt="more-than-two-walls" src="https://user-images.githubusercontent.com/57608628/149684820-0f985350-219c-4c67-8a00-e8a0dcdf77f7.png">
