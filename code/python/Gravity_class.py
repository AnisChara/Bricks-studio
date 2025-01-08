from Game_class import Game
class Gravity() :

    gravity_force = 3.6
    is_gravity = True

    def fall() :

        if not Gravity.is_gravity: return

        for entity in Game.entities : 

            if entity.weight == 0:
                continue

            if entity.is_jumping :
                continue

            
            collide = entity.collision.is_colliding_any()
            if collide and "bottom" in collide:

                entity.is_falling = False
                entity.falling_speed = 0
                entity.rect.y = collide[collide.index("bottom")-1].rect.y - entity.rect.height-1
                continue

            else:
                entity.is_falling = True
                entity.falling_speed += Gravity.gravity_force * entity.weight 
                entity.rect.y += entity.falling_speed * Game.dt
