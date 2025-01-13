from Entity_class import Entity
from Game_class import Game

class Projectile(Entity) :

    def __init__(self,id,x, y, width, height,  speed, is_collidable, shape, weight, emitter,direction,damage,image = "C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/assets/fireball.png") : #ajouter arg pour image
        super().__init__(id,image,x, y, width, height,  speed, is_collidable, shape, weight)
        self.emitter = emitter
        self.direction = direction
        self.damage = damage


    def handle_projectile():

        for entity in Game.entities:
            if not type(entity).__name__ == "Projectile": continue

            if entity.direction == 'right':
                entity.rect.x += entity.speed * 10 * Game.dt
            elif entity.direction == 'left':
                entity.rect.x -= entity.speed * 10 * Game.dt
            elif entity.direction == 'top':
                entity.rect.y -= entity.speed * 10 * Game.dt
            elif entity.direction == 'bottom':
                entity.rect.y += entity.speed * 10 * Game.dt
            elif entity.direction == 'top-right':
                entity.rect.x += entity.speed * 10 * Game.dt
                entity.rect.y -= entity.speed * 10 * Game.dt
            elif entity.direction == 'top-left':
                entity.rect.x -= entity.speed * 10 * Game.dt
                entity.rect.y -= entity.speed * 10 * Game.dt
            elif entity.direction == 'bottom-right':
                entity.rect.x += entity.speed * 10 * Game.dt
                entity.rect.y += entity.speed * 10 * Game.dt
            elif entity.direction == 'bottom-left':
                entity.rect.x -= entity.speed * 10 * Game.dt
                entity.rect.y += entity.speed * 10 * Game.dt

            collide = entity.collision.is_colliding_any()
            if collide:

                for collision in collide:

                    if type(collision).__name__ == "str": continue

                    try:
                        entity.deal_damage(collision)
                    except AttributeError:
                        pass
                    entity.die()

    def deal_damage(self, target):
        target.health -= self.damage