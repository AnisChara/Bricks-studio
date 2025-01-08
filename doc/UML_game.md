```mermaid

classDiagram 

class Game {
    +surface screen
    +clock clock
    +bool running
    +list_level levels
    +int current_level


    +surface get_screen()
    +list_entity get_entities()
    +int get_current_level()
    +list_level get_levels_list()
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
    +string shape
    +int width
    +int height
    +string image
    +int weight
    +bool is_jumping
    +bool is_falling
    +int jump_height
    +int jump_speed
    +Emitter Weapon

    +void die()
    +void track(target)
    +void move(direction, speed)
}

class Collision {
    +string type
    +entity entity
    +list_entity collision_exception
}
class Square_Collision {
    +bool is_collide(target)
}
class Circle_Collision {
    +int radius

    +bool is_collide(target)
}

class Pawn {
    +int max_health
    +int health
    +void jump()
}

class Gravity {

    +bool is_gravity
    +int gravity_force

    +void fall()
    +bool is_falling()
}

class Structure { 
    +list_movement pattern

    +void follow_pattern()
}

class offensive_entity { 

    +list_movement pattern

    +void follow_pattern()
    +void deal_damage(target)
}

class offensive_structure { 

    +void deal_damage(target)
}

class Emitter {

    +Entity Shooter
    +int x
    +int y
    +string direction

    +void set_relative_position()
    +void shoot()
}

class Projectile {
    +Emitter Emitter
    +void deal_damage(target)
}

class movement {
    +string direction
    +int speed
    +int distance
    -int goal_distance
}

Entity <|-- Pawn
Entity <|-- Structure
Entity <|-- Projectile
Collision <|-- Square_Collision
Collision <|-- Circle_Collision
Game --> Level
Level --> Entity
Emitter --> Entity
Projectile --> Emitter
Entity --> Collision
Level --> Gravity
Pawn --> offensive_entity
Structure --> offensive_structure