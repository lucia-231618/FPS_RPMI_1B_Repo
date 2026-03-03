using UnityEngine;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour
{
    #region General Variables
    [Header("General References")]
    [SerializeField] Camera fpsCam; //Ref si disparamos desde el centro de la camará
    [SerializeField] Transform shootPoint; //Ref si disparamos desde la punta del cañón
    [SerializeField] LayerMask impactLayer; //Layer con la que interactúa el raycast
    RaycastHit hit; //Almacén de la información de los objetos con los que el Raycat puede chocar

    [Header("Weapon Parameters")]
    [SerializeField] int damage = 10; //Daño del arma por bala
    [SerializeField] float range =100f; //Distancia máxima de disparo
    [SerializeField] float spread = 0; //Radio de dispersión del disparo
    [SerializeField] float shootingCooldown = 0.2f; //Tiempo entre disparos
    [SerializeField] float reloadTime = 1.5f; //Tiempo de recarga en segundos
    [SerializeField] bool allowButtonHold = false; //Si el disparo se ejecuta por click (false) o por mantener (true)

    [Header("Bullet Management")]
    [SerializeField] int ammoSize = 30; //Cantidad max de ballas por cargador
    [SerializeField] int bullsPerTap = 1; //Cantidad de ballas disparadas por cada ejecución de disparo
    int bulletsLeft; //Cantidad de balas dentro del cargador

    [Header("Feedback References")]
    [SerializeField] GameObject impactEffect; //Ref al VFX de impacto de bala

    [Header("Dev - Gun State Bools")]
    [SerializeField] bool shooting; //Indica si estamos disparando
    [SerializeField] bool canShoot; //Indica si podemos disparar en x momento del juego
    [SerializeField] bool reloading; //Indica si estamos en proceso de recarga

    #endregion

    private void Awake()
    {
        bulletsLeft = ammoSize; //Al iniciar la partida, tenemos el cargador lleno
        canShoot = true; //Al iniciar la partida podemos disparar
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        //ESTE ES EL METODO MÁS IMPORTANTE
        //SE DEFINE DISPARO POR RAYCAST -> UTILIZABLE POR CUALQUIER MECÁNICA

        //Almacenar la dirección del disparo y modificarla en caso de haber disparado
        Vector3 direction = fpsCam.transform.forward;

        //Añadir dispersión aleatoria según el valor de spread
        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);

        //DECLARACIÓN DEL RAYCAST
        //Physics.Raycast(Origen del rayo, dirección, almacén de la info del impacto, longitud del rayo, layer con la que impacta el rayo)
        if(Physics.Raycast(fpsCam.transform.position, direction, out hit, range, impactLayer))
        {
            //AQUÍ PODEMOS CODEAR TODOS LOS OBJETOS QUE QUIERO PARA LA INTERACCIÓN
            Debug.Log(hit.collider.name);
        }
    }
    #region

    public void OneShoot(InputAction.CallbackContext context)
    {
        Shoot();
    }

    public void OneReload(InputAction.CallbackContext context)
    {

    }

    #endregion
}
