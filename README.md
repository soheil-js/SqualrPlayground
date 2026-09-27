# C# Memory Read/Write Practice

A small C# project for learning the basics of **process memory reading and writing**.

This project contains two console applications:

- `MemoryTarget` — A simple process that contains a `health` variable.
- `MemoryReader` — Finds the `MemoryTarget` process and reads the health value from its memory using **Squalr Engine**.

> ⚠️ **Educational project:** This repository was created for learning and experimenting with process memory concepts in a controlled environment.

---

## 📁 Project Structure

```text
MemoryPractice/
│
├── MemoryTarget/
│   └── Program.cs
│
└── MemoryReader/
    └── Program.cs
```

---

## 🧠 How It Works

The project simulates a very simple game-like scenario.

`MemoryTarget` has a health value:

```csharp
private static int _health = 100;
```

The application then obtains the memory address of this variable using an unsafe pointer:

```csharp
fixed (int* pointer = &_health)
{
    Console.WriteLine($"Health address: 0x{(nuint)pointer:X}");
}
```

It also prints the process ID and the address of the health variable:

```text
PID: 12345
Health address: 0x7FFBD306B058
Health: 100
```

The program stays running and decreases the health value whenever a key is pressed:

```text
Health = 100
Health = 99
Health = 98
Health = 97
```

This gives us a predictable value that another process can read.

---

## 🔍 MemoryReader

The second application searches for a running process named:

```text
MemoryTarget
```

It uses:

```csharp
Process.GetProcessesByName("MemoryTarget")
```

If the process is found, its PID and name are displayed.

```text
Found process:
Name: MemoryTarget
PID : 12345
```

The process is then opened through Squalr:

```csharp
Processes.Default.OpenedProcess = process;
```

After that, `MemoryReader` reads an `int` from the target address:

```csharp
int health = Reader.Default.Read<int>(
    healthAddress,
    out bool success
);
```

If the memory read succeeds:

```text
Reading memory...

Health = 100
Health = 99
Health = 98
```

Otherwise:

```text
Error!
```

---

## 🔄 Communication Flow

The basic flow looks like this:

```text
┌─────────────────────┐
│    MemoryTarget     │
│                     │
│  int _health = 100  │
└──────────┬──────────┘
           │
           │ Stores health
           │ in its memory
           ▼
┌─────────────────────┐
│   Memory Address    │
│                     │
│  0x7FFBD306B058     │
└──────────┬──────────┘
           │
           │ Read<int>()
           ▼
┌─────────────────────┐
│    MemoryReader     │
│                     │
│  Health = 100       │
└─────────────────────┘
```

In other words:

```text
MemoryTarget
     │
     │  _health
     ▼
Process Memory
     │
     │  Read<int>(address)
     ▼
MemoryReader
```

---

## ▶️ Running the Project

### 1. Start `MemoryTarget`

Run the `MemoryTarget` console application first.

It will display something similar to:

```text
PID: 12345
Health address: 0x7FFBD306B058
Health: 100

Keep this program running...

Health = 100
```

Keep this application running.

---

### 2. Copy the Health Address

Copy the address printed by `MemoryTarget`:

```text
Health address: 0x7FFBD306B058
```

Then put that address into `MemoryReader`:

```csharp
const ulong healthAddress = 0x7FFBD306B058;
```

> The address shown above is only an example. The actual address can be different each time the target process is started.

---

### 3. Start `MemoryReader`

Run the second console application.

It searches for `MemoryTarget`, opens the process and starts reading the specified memory address.

Expected output:

```text
Found process:
Name: MemoryTarget
PID : 12345

Reading memory...
Health = 100
```

---

## 🧪 Testing

You can test the communication between the two applications by changing the value in `MemoryTarget`.

For example, press a key while `MemoryTarget` is running:

```text
Health = 100
Health = 99
Health = 98
Health = 97
```

Then check `MemoryReader`.

It should read the same value from the target process:

```text
Health = 100
Health = 99
Health = 98
Health = 97
```

This demonstrates that `MemoryReader` is reading the value directly from the memory of another running process.

---

## 🛠️ Technologies

- **C#**
- **.NET**
- **Squalr Engine**
- `System.Diagnostics.Process`
- Unsafe C# / pointers
- Inter-process memory reading

---

## ⚠️ Important Notes

### Memory addresses are not permanent

The address is currently hard-coded:

```csharp
const ulong healthAddress = 0x7FFBD306B058;
```

This means the project is intentionally kept simple for learning.

When `MemoryTarget` is restarted, the address of `_health` can change. Therefore, the address may need to be updated before running `MemoryReader`.

A future version could solve this by automatically discovering the address instead of hard-coding it.

---

### Both applications must be running

`MemoryReader` expects a process named:

```text
MemoryTarget
```

If it cannot find the process, it prints:

```text
MemoryTarget is not running.
```

--- 

## 🎯 Purpose

This repository is **not intended to be a complete memory manipulation framework**.

It is simply a small learning project created to understand how one C# process can locate another process and read data from its memory.

The project starts with a deliberately simple example:

```text
int _health = 100
```

and uses that variable to demonstrate the fundamental concept of:

```text
Variable
   ↓
Memory Address
   ↓
Another Process
   ↓
Memory Read
   ↓
Value
```

---

## 📄 License

This project is provided for educational and experimental purposes.
