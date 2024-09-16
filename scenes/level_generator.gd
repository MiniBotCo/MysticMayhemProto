extends Node2D

@export var LevelDivisions: Vector2 = Vector2(3, 3)
@export var PlatformRandomOffsetRange: Vector2 = Vector2(4, 1)
@export var PlatformMinimum: int = 5
@export var PlatformMaximum: int = 9
@export var PlatformLengthMin: int = 5
@export var PlatformLengthMax: int = 8

# Called when the node enters the scene tree for the first time.
func _ready():
	var level_size = Vector2(int(get_window().size.x/($Camera2D.zoom.x * $Platforms.tile_set.tile_size.x)), int(get_window().size.y/($Camera2D.zoom.y * $Platforms.tile_set.tile_size.y)))
	print(level_size)
	var platform_count = randi_range(PlatformMinimum, PlatformMaximum)
	var platform_divisions = []
	
	for x in range(LevelDivisions.x):
		for y in range(LevelDivisions.y):
			platform_divisions.append(Vector2i(x,y))
	
	platform_divisions.shuffle()
	platform_divisions.resize(platform_count)
	
	for pos in platform_divisions:

		var x = pos.x*(level_size.x / LevelDivisions.x) + randi_range(0, PlatformRandomOffsetRange.x)
		var y = (pos.y)*(level_size.y / LevelDivisions.y) + (level_size.y / LevelDivisions.y) / 2 + randi_range(0, PlatformRandomOffsetRange.y)
		for len in range(randi_range(PlatformLengthMin, PlatformLengthMax)):
			$Platforms.set_cell(Vector2i(x + len, y), 0, Vector2i(10, 3))


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
