---
trigger: always_on
---

---
trigger: Always On 
---

# Programming Style Rules (C, C++, C#)

## 1. Design Principles

- Follow **Divide and Conquer**: break complex problems into smaller, manageable parts.
- Apply **SOLID principles**:
  - SRP: one responsibility per class/function
  - OCP, LSP, ISP, DIP must be respected
- Use **modularity**: separate logically independent components.
- Follow **top-down design**: start from high-level logic and refine.
- Apply **KISS (Keep It Simple)**: avoid unnecessary complexity.
- Apply **DRY (Don't Repeat Yourself)**: eliminate code duplication.

---

## 2. Functions

- Each function must perform **one clearly defined task**.
- Functions must be:
  - short
  - readable
  - not deeply nested
- Avoid:
  - long functions
  - chained function calls
  - unnecessary parameters
- Use parameters strictly for data passing.
- `main()` must:
  - be minimal
  - act as a coordinator (dispatcher), not contain logic

---

## 3. Naming Conventions

- Names must be **meaningful and descriptive**.
- Use **English only**.
- Forbidden:
  - unclear abbreviations (`tmp`, `x`, `calc`)
- Use consistent naming style:
  - `camelCase` or `snake_case`
- Rules:
  - Classes → `PascalCase`
  - Functions → verbs (`calculateSum`, `getData`)
  - Boolean variables → `isReady`, `hasValue`
  - Constants → `UPPER_CASE`
- Names must be unique within scope.

---

## 4. Variables and Constants

- Declare variables **as locally as possible**.
- Always initialize variables at declaration.
- Avoid uninitialized variables.
- Minimize use of global variables (preferably avoid entirely).

---

## 5. Control Flow

- Use only basic control structures:
  - sequence
  - branching (`if/else`)
  - loops
- Every `if` must include an `else`.
- Avoid infinite loops:
  - loops must have clear termination conditions
- Keep control flow simple and readable.

---

## 6. Code Structure

- Program must be **modular**.
- Each module:
  - has one responsibility
  - has a clear interface
- Minimize coupling between modules.
- Avoid side effects where possible.

---

## 7. Documentation

- Use **Doxygen-compatible comments**.
- Document:
  - all public classes and methods
  - complex logic sections
- Comments must explain:
  - **why**, not **what**
- Avoid redundant or obvious comments.

---

## 8. Anti-Patterns (Forbidden)

- Global variables
- Code duplication
- Long functions
- Unclear naming
- Macros (`#define`)
- Deeply nested logic

---

## 9. OOP Rules (C#)

- Strictly follow **SOLID principles**.
- Use **encapsulation**.
- Classes must be:
  - small
  - focused
  - single-responsibility (SRP)
## 10. Clean Code Principles

- Code must follow **Clean Code** practices:
  - Readability is a priority over cleverness.
  - Code should be self-explanatory.
  - Avoid unnecessary comments if code can be written clearly.
  - Each line of code must have a clear purpose.
  - Functions and classes should be easy to understand without additional explanation.

- Avoid:
  - overly complex logic
  - hidden side effects
  - unclear or "magic" behavior

---

## 11. Code Formatting

- Code must be consistently formatted and visually structured.

### Braces Style

- Always use **explicit braces `{}`**, even for single-line statements.
- Braces must follow this style:

```csharp
if (condition)
{
    // code
}
else
{
    // code
}