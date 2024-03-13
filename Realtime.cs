using UnityEngine; 
using Firebase; 
using Firebase.Database; 
using Firebase.Extensions; 
using Unity.VisualScripting; 
public class Realtime : MonoBehaviour 
{

  private FirebaseApp _app; // Almacena la instancia de la aplicación Firebase.
  private FirebaseDatabase _db; // Almacena la instancia de la base de datos Firebase.
  private DatabaseReference _refJugadores; // Referencia a la colección "Jugadores" en la base de datos.
  private DatabaseReference _refPrefabs; // Referencia a la colección "Prefabs" en la base de datos.
  private DatabaseReference _refAA002; // Referencia al jugador con identificador "AA02".
  private DatabaseReference _refp1; // Referencia a pickUp
  private DatabaseReference _refp2; // // Referencia a pickUp2
  private DatabaseReference _refp3; // // Referencia a pickUp3
  public GameObject ondavital; // Almacena el player
  private float _i; // Contador
  public GameObject pickUp; // pickUp
  public GameObject pickUp2; // pickUp2
  public GameObject pickUp3; // pickUp3
  public GameObject angi;// Declaro variable GAmeOBject


  void Start() // Método Start, llamado antes del primer fotograma.
  {
      // Inicializamos el contador.
      _i = 0;

      // Realizamos la conexión a Firebase.
      _app = Conexion();

      // Obtenemos el Singleton de la base de datos.
      _db = FirebaseDatabase.DefaultInstance;


      // Obtenemos la referencia a TODA la base de datos.
     // DatabaseReference reference = _db.RootReference;

      // Definimos la referencia a Jugadores.
      _refJugadores = _db.GetReference("Jugadores");
      
      // Definimos la referencia a AA02.
      _refAA002 = _db.GetReference("Jugadores/AA02");

      // Definimos la referencia a Prefabs.
      _refPrefabs = _db.GetReference("Prefabs");

      // Definimos las referencias a los prefabricados pickUp, pickUp2, pickUp3, pickUp4 y pickUp5.
      _refp1 = _db.GetReference("Prefabs/p1");
      _refp2 = _db.GetReference("Prefabs/p2");
      _refp3 = _db.GetReference("Prefabs/p3");


      // Recogemos todos los valores de Jugadores.
      _refJugadores.GetValueAsync().ContinueWithOnMainThread(task => {
              if (task.IsFaulted) {
              }
              else if (task.IsCompleted) {
                  DataSnapshot snapshot = task.Result;
                  RecorreResultado(snapshot);

              }
          });


      // Recogemos todos los valores de Prefabs.
      _refPrefabs.GetValueAsync().ContinueWithOnMainThread(task => {
          if (task.IsFaulted) {
          }
          else if (task.IsCompleted) {
              DataSnapshot snapshot = task.Result;
              RecorreResultado(snapshot);

          }
      });

      // Eventos
      _refAA002.ValueChanged += HandleValueChanged;
      _refp1.ValueChanged += HandleValueChanged_prefabs;
      _refp2.ValueChanged += HandleValueChanged_prefabs;
      _refp3.ValueChanged += HandleValueChanged_prefabs;

      // Añadimos un nodo a la base de datos.
      AltaDevice();
  }


  // Conexión a Firebase 
  FirebaseApp Conexion()
  {
      FirebaseApp firebaseApp = null;
      FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
      {
          var dependencyStatus = task.Result;
          if (dependencyStatus == DependencyStatus.Available)
          {
              // Creamos y almacenamos una referencia a nuestra FirebaseApp.
              firebaseApp = FirebaseApp.DefaultInstance;
              // Establecemos una marca aquí para indicar si Firebase está listo para ser utilizado por nuestra aplicación.
          }
          else
          {
              Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
              // La SDK de Firebase Unity no es segura para usar aquí.
              firebaseApp = null;
          }
      });

      return firebaseApp;
  }


  void HandleValueChanged(object sender, ValueChangedEventArgs args) {
      if (args.DatabaseError != null) {
          Debug.LogError(args.DatabaseError.Message);
          return;
      }
      MuestroJugador(args.Snapshot);
      float escala = float.Parse(args.Snapshot.Child("puntos").Value.ToString());
      Vector3 cambioEscala = new Vector3(escala, escala, escala);
      ondavital.transform.localScale = cambioEscala;
  }


  void HandleValueChanged_prefabs(object sender, ValueChangedEventArgs args) {
      if (args.DatabaseError != null) {
          Debug.LogError(args.DatabaseError.Message);
          return;
      }


      // Obtén el nombre del pickup desde la referencia que disparó el evento
      string pickupName = args.Snapshot.Key;


      // Obtenemos la posición del pickup desde la base de datos
      float x = float.Parse(args.Snapshot.Child("x").Value.ToString());
      float y = float.Parse(args.Snapshot.Child("y").Value.ToString());
      float z = float.Parse(args.Snapshot.Child("z").Value.ToString());


      // Actualizamos la posición del pickup correspondiente en Unity
      switch (pickupName) {
          case "p1":
              pickUp.transform.position = new Vector3(x, y, z);
              break;
          case "p2":
              pickUp2.transform.position = new Vector3(x, y, z);
              break;
          case "p3":
              pickUp3.transform.position = new Vector3(x, y, z);
              break;
          default:
              Debug.LogError("Pickup desconocido: " + pickupName);
              break;
      }
  }


  // Modifica el método RecorreResultado para manejar los datos de posición de los prefabs.
  void RecorreResultado(DataSnapshot snapshot)
  {
      foreach(var resultado in snapshot.Children) // Iteramos sobre los nodos hijos del snapshot.
      {
          Debug.LogFormat("Key = {0}", resultado.Key);  // Imprimimos la clave del nodo.
          foreach(var levels in resultado.Children) // Iteramos sobre los hijos de cada nodo.
          {
              Debug.LogFormat("(key){0}:(value){1}", levels.Key, levels.Value); // Imprimimos la clave y el valor.
 
          }
      }
  }


  // Método para mostrar un jugador.
  void MuestroJugador(DataSnapshot jugador)
  {
      foreach (var resultado in jugador.Children) // Iteramos sobre los hijos del jugador.
      {
          Debug.LogFormat("{0}:{1}", resultado.Key, resultado.Value); // Imprimimos la clave y el valor.
      }
  }


  // Método para dar de alta un nodo con un identificador único.
  void AltaDevice()
  {
      _refJugadores.Child(SystemInfo.deviceUniqueIdentifier).Child("nombre").SetValueAsync("Mi dispositivo");
  }


  void Update() 
  {
      
      double playerx = ondavital.transform.position.x; // Obtenemos la posición x del objeto.
      double playery = ondavital.transform.position.y; // Obtenemos la posición y del objeto.
      
      // Actualizamos la base de datos en cada fotograma.
      _refJugadores.Child("AA01").Child("puntos").SetValueAsync(_i);
      _i = _i + 0.01f; 
      
      //Actualizamos las posicions de x y en y en la base de datos
      _refJugadores.Child("AA01").Child("x").SetValueAsync(playerx);
      _refJugadores.Child("AA01").Child("y").SetValueAsync(playery);
      
  }
}

