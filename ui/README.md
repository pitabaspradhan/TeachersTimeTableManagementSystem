# Teachers TimeTable UI


**Running from Code Sandbox *** 
 -- if you run it from code sandbox this is online setup below setup is not needed(Running from local system), you just click the below link

[ Running from Code Sandbox *](https://codesandbox.io/p/devbox/icy-wind-m584ng?file=%2Fsrc%2Findex.css%3A10%2C1)

### Here I have just hardcoded the the response just to diplaying how the UI looks like if fetch the data from the teacher timetable database in a genric format for any teacher's timetable. It display all perid in a grid format.

**Running from Local System *** 

This folder contains the **frontend user interface** for the **Teachers TimeTable Management System**.

The Teachers TimeTable UI is a web-based application that allows users to:
- Upload teachers’ timetable files (image / PDF)
- View extracted and structured timetable data
- Interact with backend APIs for processing and persistence
- Review and manage timetable information through a modern UI

The UI is implemented as a **single-page application (SPA)**.

---

## Technology Stack

- React
- TypeScript
- Vite
- SWC
- ESLint

---

## Prerequisites

Before running the UI locally, ensure the following tools are installed:

### 1. Node.js
- Install **Node.js 18+** (recommended: Node 20 LTS)
- Download from: https://nodejs.org
- Verify installation:
```bash or command prompt 
node -v
npm -v
```

### 2. Visual Studio Code (Recommended)
- Download from: https://code.visualstudio.com
- Recommended extensions:
  - ESLint
  - Prettier
  - ES7+ React Snippets

---

## Setup & Run (Local Development)

### Step 1: Navigate to UI folder
From the repository root:

```bash
cd ui
```

Ensure `package.json` exists in this folder.

---

### Step 2: Install dependencies

```bash
npm install
```

This installs all required frontend dependencies.

---

### Step 3: Run the development server

```bash
npm run dev
```

After successful startup, you will see output similar to:

```text
VITE v5.x.x ready
➜ Local: http://localhost:5173/
```

Open your browser and navigate to:

```
http://localhost:5173
```

---

## Environment Variables

Frontend environment variables should be placed in:

```text
ui/.env
```

Example:

```env
VITE_API_BASE_URL=http://localhost:5000
```

> Note: `.env` files are ignored by Git and should not be committed.

---

## Backend Dependency

- This UI depends on backend APIs being available
- Ensure backend services are running before using the UI
- Refer to `backend/README.md` for backend setup instructions

---

## Common Commands

```bash
npm run dev       # Start development server
npm run build     # Create production build
npm run preview   # Preview production build
npm run lint      # Run ESLint
```

---

## Notes

- The UI communicates with backend services exclusively via HTTP APIs
- No business logic exists in the UI layer
- All core domain logic resides in backend services
