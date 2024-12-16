```mermaid

classDiagram 

class Game {
    +surface screen
    +clock clock
    +bool running
    +list_entity entities
    +list_level levels
}

class Level {
    +string background
    +list_entity entities
    +int score

    +void load(entities)
}

class Entity {
    +int id
    +collision collision
    +bool is_collidable
    +int x
    +int y
    +int velocity
    +string direction
    +int width
    +int height
    +string image
    +Gravity gravity
    +Jump jump
    +Emitter Weapon

    +void die()
    +void track(target)
    +void move()
}

class Collision {
    +string type
    +entity entity
    +list_entity collision_exception
}
class Square_Collision {
    +bool is_collide()
}
class Circle_Collision {
    +int radius

    +bool is_collide()
}

class Pawn {
    +int max_health
    +int health
}

class Gravity {

    +bool is_falling
    +int falling_initial_speed
    +int falling_maximum_speed
    +int time_for_maximum_falling_speed

    +void fall()
}

class Jump {

    +bool is_jumping
    +int jump_height
    +int jump_speed

    +void jump()
}

class Ennemy {
    +list_movement pattern

    +void follow_pattern()
}

class Structure { 
    +list_movement pattern

    +void interact()
    +void follow_pattern()
}

class Emitter {

    +Entity Shooter
    +int x
    +int y
    +string direction
}

class Projectile {
    +Emitter Emitter
    +int velocity
}

class movement {
    +string direction
    +int speed
    +int distance
    -int goal_distance
}

Entity <|-- Pawn
Pawn <|-- Ennemy
Entity <|-- Structure
Entity <|-- Projectile
Collision <|-- Square_Collision
Collision <|-- Circle_Collision
Game --> Level
Level --> Entity
Emitter --> Entity
Projectile --> Emitter
Entity --> Collision
Entity --> Gravity
Entity --> Jump