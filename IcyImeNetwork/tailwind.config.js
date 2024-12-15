/** @type {import('tailwindcss').Config} */
module.exports = {
   content: ['./**/*.{razor,html}'],
  theme: {
      extend: {
          colors: {
              teal: {
                  '100': '#08040b',
                  '200': '#b748d5',
                  '300': '#5e0ca6',
                  '400': '#747494',
              }
          }
      },
  },
  plugins: [],
}

