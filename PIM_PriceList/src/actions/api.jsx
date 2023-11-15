import axios from "axios";

const baseURL = "https://localhost:7149/"

export default {
    ItemRegister(url = baseURL + 'Item/') {
        return {
            fetchAll : () => axios.get(url),
            fetchByItemName : itemName => axios.get(url + itemName),
            create : newRecord => axios.post(url + 'createitem', newRecord),
            update : (itemName, updateRecord) => axios.put(url + 'updateitem?itemName='+itemName, updateRecord),
            delete : itemName => axios.delete(url + 'deleteitem?itemName=' + itemName)
        }
    }
}