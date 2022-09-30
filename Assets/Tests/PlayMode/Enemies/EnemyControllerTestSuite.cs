using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FromWithin.Enemies;
using Object = UnityEngine.Object;

namespace Tests.PlayMode.Enemies
{
    public class EnemyControllerTestSuite
    {
        private EnemyController _controller;

        [SetUp]
        public void Setup()
        {
            var testObject = Object.Instantiate(
                Resources.Load<GameObject>("Enemies/Prefabs/Golem A"));
            _controller = testObject.GetComponent<EnemyController>();

            // TODO: Instead of disabling gravity, add a floor
            testObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_controller.gameObject);
        }

        [UnityTest]
        public IEnumerator TestEnemyMovesToTargetAndStops()
        {
            var transform = _controller.transform;

            // Act
            _controller.MoveSpeed = 10f;
            var targetPosition = new Vector2
            {
                x = 4f,
                y = 0f
            };
            _controller.Move(targetPosition);

            yield return new WaitForSeconds(1f);

            // Assert
            // ReSharper disable once Unity.InefficientPropertyAccess
            var position = transform.position;
            Assert.AreEqual(targetPosition.x, position.x, 0.1f);
            Assert.AreEqual(targetPosition.y, position.y, float.Epsilon);
        }

        [UnityTest]
        public IEnumerator TestEnemyOnlyMovesHorizontally()
        {
            // Act
            var transform = _controller.transform;
            var initialPosition = transform.position;
            _controller.Move(new Vector2
            {
                x = 1f,
                y = _controller.JumpThreshold
            });

            yield return new WaitForSeconds(0.1f);

            // Assert
            // ReSharper disable once Unity.InefficientPropertyAccess
            var position = transform.position;
            Assert.Greater(position.x, initialPosition.x);
            Assert.AreEqual(position.y, initialPosition.y);
        }

        [UnityTest]
        public IEnumerator TestEnemyJumpsWhenYVectorIsGreaterThanLimit()
        {
            // Act
            _controller.Move(Vector2.up);
            yield return new WaitForSeconds(0.1f);

            // Assert
            Assert.True(_controller.IsJumping);
        }

        [UnityTest]
        public IEnumerator TestEnemyDoesNotJumpWhenYVectorIsLessThanLimit()
        {
            // Act
            _controller.Move(Vector2.up * _controller.JumpThreshold);
            yield return new WaitForSeconds(0.1f);

            // Assert
            Assert.False(_controller.IsJumping);
        }
    }
}