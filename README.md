# MonoECS

![](monoecs.gif)

An Entitiy Component System for MonoGame,

(systems are called services oops)

The ECS Library is nearly finished, there are more optimizations that can be done. Like adding a Archetypes organizer for components which is faster when calculating NOT and ONLY queries and maybe the ALL

## Examples
### Component
```c#
class Transform : Component {
    public Vector2 position;
    public Vector2 direction;
    public Vector2 scale;
}
```
### Entity
```c#
World world = new World;
Entity entity = world.CreateEntity()
    .AddComponent<Transform>()
    .AddComponent<Sprite>();

entity.GetComponent<Transform>().position = new Vector2(10,10);
entity.GetComponent<Sprite>().texture = Content.Load<Texture2D>("Person");
```
### Query
``` c#
Type[] include = new Type[] {
    typeof(transform)
};
Type[] exclude = new Type[] {
    typeof(sprite)
};
Query q1 = Query.All(include);
Query q2 = Query.All(include).Not(exclude);
Query q3 = Query.Only(include);
```
### Service
```c#
class ExampleService : Service{
    static Type[] includeComponents = new Type[]{typeof(Transform)};

    public ExampleService(){
        query = Query.All(includeComponents);
    }

    public override void Execute(Entity[] entities){
        Parallel.foreach(entities, entity => {
            Transform transform = entitiy.GetComponent<Transform>();
            // game logic that manipulates included components
        })
    }
}
```

## Yaml Parser

The engine can load entities from a yaml file. A file can contain mutliple entities and is made up of the components that store that entities data. In the future a tool may be made to make the construction of entities easier. E.G a web app that can create entities by selecting the components and filling in the values.

## Example

``` yaml
- Sprite:
    texture: bush
    size:
      X: 16
      Y: 16
    scale: 4
    flipX: false
    flipY: false
    rotation: 0
  Transform:
    position:
      X: 32
      Y: 32
    direction:
      X: 0
      Y: 0
    speed: 0
  Flammable:
    isOnFire: false
    percentChanceToSpread: 10
    damagePerUpdate: 1
```

ECS To-Do
 - [ ] Archtypes

Monogame services
 - [ ] Colliders
 - [ ] Actors
 - [ ] Actor Parent-Child relationships  
 - [ ] UI components 

Problems
 - Entities loaded from files can't have actions, can probably get around this by listing the name of the action in a class?

