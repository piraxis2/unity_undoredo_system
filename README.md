# Undo Redo System

Godot의 `UndoRedoManager`에서 영감을 받은 유니티용 범용 Undo/Redo 시스템입니다.

## 설치 방법

이 폴더(`UndoRedo`)는 로컬 패키지로 사용할 수 있습니다. `Packages` 폴더로 옮기거나 `Assets` 폴더 내에 그대로 두고 사용할 수 있습니다.

## 사용법

### 기본 사용법

Undo/Redo 컨텍스트가 필요한 곳마다 `UndoRedoHelper`를 인스턴스화하여 사용합니다.

```csharp
using UndoRedoSystem;

public class LevelEditor : MonoBehaviour
{
    // 이 에디터를 위한 별도의 Undo/Redo 히스토리 생성 (최대 100개 저장)
    private UndoRedoHelper _history = new UndoRedoHelper(100);

    public void MoveObject(Transform target, Vector3 newPos)
    {
        Vector3 oldPos = target.position;

        // 트랜잭션 생성 및 커밋
        _history.CreateTransaction("Move Object")
            .AddDo(() => target.position = newPos)
            .AddUndo(() => target.position = oldPos)
            .Commit();
    }

    public void Undo() => _history.Undo();
    public void Redo() => _history.Redo();
}
```

### 트랜잭션 생성 (Godot 스타일)

`CreateTransaction`을 사용하여 Do(실행)와 Undo(취소) 액션을 체이닝 방식으로 구성할 수 있습니다.

```csharp
_history.CreateTransaction("Action Name")
    .AddDo(() => Debug.Log("실행"))
    .AddUndo(() => Debug.Log("취소"))
    .Commit();
```

### 일반 커맨드 사용

`ICommand`를 직접 구현하거나 `GenericCommand`를 사용할 수도 있습니다.

```csharp
var cmd = new GenericCommand(
    executeAction: () => Debug.Log("무언가 실행"),
    undoAction: () => Debug.Log("무언가 취소")
);
_history.ExecuteCommand(cmd);
```

## 주요 기능

- **다중 컨텍스트 지원**: 싱글턴이 아니므로 게임의 여러 부분(예: 레벨 에디터, 캐릭터 커스터마이징 등)에서 서로 다른 Undo 히스토리를 가질 수 있습니다.
- **트랜잭션 빌더**: Do/Undo 액션을 쉽게 연결할 수 있는 Fluent API를 제공합니다.
- **커맨드 패턴**: 인터페이스 기반 설계로 복잡한 커맨드 로직도 구현 가능합니다.
- **히스토리 관리**: 설정 가능한 제한 크기를 가진 히스토리 스택을 자동으로 관리합니다.
