import '@mdi/font/css/materialdesignicons.css';
import 'vuetify/styles';
import { createVuetify, type ThemeDefinition} from 'vuetify';

const MainTheme : ThemeDefinition = {
  dark:false,
  colors: {
    primary:"#0C0C0C",
    background: "#f4f4f4",
    surface: "#f4f4f4",
    mainBlack: "#0C0C0C",
    secondary: "#484848",
    mainRed: "#FF203B",
    mainBlue: "#02C1C9",
  }
};

const LightTheme : ThemeDefinition = {
  dark:false,
};

export default defineNuxtPlugin((app) => {
  const vuetify = createVuetify({
    theme: {
      defaultTheme: 'MainTheme',
      themes:{
        MainTheme
      }
    }
  })
  app.vueApp.use(vuetify)
})