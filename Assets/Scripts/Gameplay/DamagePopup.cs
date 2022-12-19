using System.Collections;
using TMPro;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshPro damageText = default;
        [SerializeField] private Vector2 offset = default;
        [SerializeField] private float time = 0.5f;


        public void Initialize(float damage, Vector2 position, bool randomize = true)
        {
            damageText.text = damage.ToString("0");
            transform.position = position;

            gameObject.SetActive(true);

            StartCoroutine(FloatAway(offset, time));

            IEnumerator FloatAway(Vector2 offset, float time = 0.5f, bool randomize = true)
            {
                Vector3 startPos = position;
                Vector2 targetPos = position + offset;
                if (randomize) targetPos += Random.insideUnitCircle * offset.sqrMagnitude;
                float elapsedTime = 0;
                while (elapsedTime < time)
                {
                    transform.position = Vector3.Lerp(startPos, targetPos, (elapsedTime / time));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                gameObject.SetActive(false);
            }
        }
    }
}