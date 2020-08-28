# Learn Unity, Ruby's Adventure: 2D Beginner Tutorial

- ### [learn.unity.com/course/beginning-2d-game-development](https://learn.unity.com/course/beginning-2d-game-development)

## NOTES:
  - ### Tile Palette Shortcuts:
    1. (<u>S</u>)elect
    2. (<u>M</u>)ove
    3. Paint(<u>B</u>)rush
    4. <u>U</u>: Fill Box
    5. P(<u>I</u>)cker
    6. <u>D</u>: Eraser
    7. <u>G</u>: Fill

  - ### Jittering Issue:
    - You need to move objects that have Rigidbody with MovePosition function and run all physics related calculations inside FixedUpdate.

  - Use Mathf.Approximately instead of == because the way computers store float numbers means there is a tiny loss in precision.

  - Normalize vectors that store direction because length is not important, only the direction is.