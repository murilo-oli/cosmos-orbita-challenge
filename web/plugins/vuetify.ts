import '@mdi/font/css/materialdesignicons.css';
import 'vuetify/styles';
import { createVuetify, type ThemeDefinition} from 'vuetify';

const MainTheme : ThemeDefinition = {
  dark:false,
  colors: {
    primary:"#FF203B",
    background: "#f4f4f4",
    surface: "#f4f4f4",
    mainBlack: "#0C0C0C",
    secondary: "#484848",
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