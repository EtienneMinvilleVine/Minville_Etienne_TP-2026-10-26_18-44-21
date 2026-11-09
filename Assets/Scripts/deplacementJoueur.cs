using UnityEngine;


public class deplacementJoueur : MonoBehaviour
{
    [SerializeField] float vitesseDeplacement;

     [SerializeField] float vitesseRotation = 100f;

    [SerializeField] float gravite;

  

    [SerializeField] float decelerationSpeciale;

    [SerializeField] float hauteurSaut;

    Vector3 vitesseDeplacementGeneral;  

    Vector3 vitesseDeplacementVertical;

    Vector3 vitesseDeplacementSpecial;

    CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    public Vector3 CalculerDeplacementGeneral(Vector2 joueurInput)
    {
        vitesseDeplacementGeneral = transform.forward * joueurInput.y * vitesseDeplacement;
        return vitesseDeplacementGeneral;
    }

    public Vector3 CalculerDeplacementVertical()
    {
        if (characterController.isGrounded && vitesseDeplacementVertical.y < 0)
        {
           vitesseDeplacementVertical.y = -2f;
        }
        else
        {
            vitesseDeplacementVertical.y += gravite * Time.deltaTime;
        }

        return vitesseDeplacementVertical;
    }

    public Vector3 CalculerDeplacementSpecial()
    {
        vitesseDeplacementSpecial = Vector3.MoveTowards(vitesseDeplacementSpecial, Vector3.zero, decelerationSpeciale *Time.deltaTime);
        return vitesseDeplacementSpecial;
    }

    public void Sauter()
    {
        if (characterController.isGrounded)
        {
            vitesseDeplacementVertical.y = Mathf.Sqrt(hauteurSaut * -2f * gravite);
        }
    }

    public void Dash()
    {
        vitesseDeplacementSpecial = transform.forward * 5;
    }

    public void AppliquerForceSpeciale(Vector3 direction, float force)
    {
        vitesseDeplacementSpecial += direction * force;
    }

    public void MettreAJourDeplacement(Vector3 direction)
    {
        characterController.Move(direction * Time.deltaTime);
    }

     public void MettreAJourRotation(float directionLaterale)
    {
        transform.Rotate(Vector3.up * directionLaterale * vitesseRotation * Time.deltaTime);
    }



    // Update is called once per frame
    void Update()
    {
       
    }
}
