import './App.css';
import store from './actions/store';
import { Provider } from 'react-redux';
import Items from './components/Items';
import { Container } from '@mui/material';
import { Toaster } from 'react-hot-toast';
function App() {
  return (
    <Provider store={store}>
      <Container>
        <Toaster
          
          toastOptions={{
            style: {
              color: 'white',
              background: 'green'
            }
          }}
        />
        <Items />
      </Container>
    </Provider>
  );
}

export default App;