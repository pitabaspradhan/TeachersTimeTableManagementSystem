import { useState } from 'react'

import './App.css'

const columns = [
  "Day",
  "Reg",
  "9–9:30",
  "9:30–10",
  "10–10:15",
  "Break",
  "10:35–11",
  "11–11:55",
  "Lunch",
  "1–1:15",
  "1:15–2",
  "2–3",
  "Story",
];

const timetable = [
  {
    day: "Monday",
    cells: [
      "Registration",
      "RWI",
      "Maths",
      "Assembly",
      "",
      "Maths Con",
      "English",
      "",
      "Handwriting",
      "Maths Meeting",
      "Science",
      "Comprehension / Library",
    ],
  },
  {
    day: "Tuesday",
    cells: [
      "Registration",
      "RWI",
      "Maths",
      "",
      "",
      "Maths Con",
      "English",
      "",
      "Handwriting",
      "PHSE\n(Anti-Bullying)",
      "Computing",
      "",
    ],
  },
  {
    day: "Wednesday",
    cells: [
      "Registration",
      "RWI",
      "Maths",
      "Assembly",
      "",
      "Maths Con",
      "History",
      "",
      "Handwriting",
      "English",
      "Music",
      "",
    ],
  },
  {
    day: "Thursday",
    cells: [
      "Registration",
      "RWI",
      "PE",
      "Singing Assembly",
      "",
      "PE",
      "",
      "",
      "Handwriting",
      "English\n(Sentence Stacking)",
      "Maths (TTRS)",
      "",
    ],
  },
  {
    day: "Friday",
    cells: [
      "Registration",
      "Celebration Assembly",
      "RWI",
      "",
      "",
      "English",
      "RE",
      "",
      "Handwriting",
      "Maths",
      "Art",
      "",
    ],
  },
];
function App() {
   return (
    <div className="wrapper">
      <table className="timetable">
        <thead>
          <tr>
            {columns.map((col) => (
              <th key={col}>{col}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {timetable.map((row) => (
            <tr key={row.day}>
              <td className="day">{row.day}</td>
              {row.cells.map((cell, i) => (
                <td key={i} className={getClass(cell)}>
                  {cell}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
export default App;

function getClass(text: string) {
  if (!text) return "empty";
  if (text.includes("Assembly")) return "assembly";
  if (text.includes("RWI")) return "rwi";
  if (text.includes("Maths")) return "maths";
  if (text.includes("English")) return "english";
  if (text.includes("PE")) return "pe";
  if (text.includes("Science")) return "science";
  if (text.includes("History")) return "history";
  if (text.includes("Music")) return "music";
  if (text.includes("Art")) return "art";
  if (text.includes("PHSE")) return "phse";
  return "default";
}