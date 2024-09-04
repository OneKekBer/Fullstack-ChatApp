/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        bg: "var(--bg)",
        bgSecondary: "var(--bg-secondary)",
        h1: "var(--h1)",
        p: "var(--p)",
        button: "var(--button)",
        buttonText: "var(--button-text)"
      }
    },
  },
  plugins: [],
}