namespace TeachersTimeTable.Application.Ai;

public static class GeminiPromptBuilder
{
    public static string BuildTimetableExtractionPrompt(string ocrText)
    {
        if (string.IsNullOrWhiteSpace(ocrText))
            throw new ArgumentException("OCR text cannot be empty", nameof(ocrText));

        return $$"""
You are a data extraction engine.
You do not explain.
You do not summarize.
You do not add commentary.
You only extract structured data.

TASK:
Extract a school teacher timetable from the OCR text below.

IMPORTANT RULES:
- Do NOT invent or infer any data.
- Do NOT skip any detected lesson, break, or activity.
- Do NOT merge time slots.
- Do NOT group results by day.
- Preserve the order exactly as it appears in the OCR text.
- If a subject name is unclear, output it exactly as written.

OUTPUT FORMAT:
Return ONLY a valid JSON array.
Each element MUST strictly follow this schema:

{
  "day": "Monday | Tuesday | Wednesday | Thursday | Friday",
  "subject": "string",
  "startTime": "hh:mm am/pm",
  "endTime": "hh:mm am/pm"
}

FORMAT RULES:
- Use English day names only.
- Use 12-hour time format with lowercase am/pm.
- Do NOT include markdown.
- Do NOT include explanations.
- Do NOT include any text outside the JSON array.

OCR TEXT:
{{ocrText}}
""";
    }
}
