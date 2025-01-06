from Game_class import Game
class Gravity() :

    gravity_force = 3.6


    def __init__(self) : #ajouter arg pour image
        self.is_gravity = True

    def fall() :
        for entity in Game.entities : 
            if entity.collision.is_colliding() == "down" :
                entity.is_falling = False
                entity.falling_speed = 0
                continue

            if entity.collision.is_colliding() != "down" :
                entity.is_falling = True
                entity.falling_speed += Gravity.gravity_force * entity.weight 
                entity.rect.y += entity.falling_speed * Game.dt
