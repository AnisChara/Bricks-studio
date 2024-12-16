
class Game() :

    def __init__(self,screen,clock) : #ajouter arg pour image
        super().__init__()
        self.screen = screen
        self.clock = clock
        self.running = True
        self.dt = 0
        self.entities = []
        self.levels = []
