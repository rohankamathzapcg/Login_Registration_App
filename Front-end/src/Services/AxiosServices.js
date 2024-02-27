const axios = require("axios").default;

const AxiosServices = {
    post: function(url, data, isRequired = false, header) {
        return axios.post(url, data, isRequired && header);
    }
};

export default AxiosServices;
