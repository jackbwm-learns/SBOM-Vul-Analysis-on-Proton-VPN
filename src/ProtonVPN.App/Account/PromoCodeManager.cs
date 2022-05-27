﻿/*
 * Copyright (c) 2022 Proton Technologies AG
 *
 * This file is part of ProtonVPN.
 *
 * ProtonVPN is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * ProtonVPN is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with ProtonVPN.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Net.Http;
using System.Threading.Tasks;
using ProtonVPN.Common.Abstract;
using ProtonVPN.Core.Api;
using ProtonVPN.Core.Api.Contracts;

namespace ProtonVPN.Account
{
    public class PromoCodeManager : IPromoCodeManager
    {
        private readonly IApiClient _apiClient;

        public PromoCodeManager(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Result> ApplyPromoCodeAsync(string code)
        {
            try
            {
                PromoCodeRequestData requestData = new() { Codes = new[] { code } };
                ApiResponseResult<BaseResponse> response = await _apiClient.ApplyPromoCodeAsync(requestData);
                return response.Success ? Result.Ok() : Result.Fail(response.Error);
            }
            catch (HttpRequestException e)
            {
                return Result.Fail(e.Message);
            }
        }
    }
}