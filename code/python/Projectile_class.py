from Entity_class import Entity

class Projectile(Entity) :

    def __init__(self, x, y, width, height, velocity, emitter) : #ajouter arg pour image
        super().__init__(x, y, width, height)
        self.velocity = velocity
        self.emitter = emitter