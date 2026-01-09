using HDI.Application.DTOs.Keyword;
using HDI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HDI.WebApi.Controllers;

public class KeywordsController(IKeywordService _keywordService) : BaseController
{
    [HttpGet("agreement/{agreementId}")]
    public async Task<IActionResult> GetByAgreement(int agreementId) 
        => ActionResultInstance(await _keywordService.GetKeywordsByAgreementIdAsync(agreementId));
    [HttpPost]
    public async Task<IActionResult> Create(CreateKeywordRequest request) 
        => ActionResultInstance(await _keywordService.AddKeywordAsync(request));

    [HttpPatch("{id}/weight")]
    public async Task<IActionResult> UpdateWeight(int id, [FromBody] decimal newWeight) 
        => ActionResultInstance(await _keywordService.UpdateKeywordWeightAsync(id, newWeight));
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) 
        => ActionResultInstance(await _keywordService.DeleteKeywordAsync(id));
}